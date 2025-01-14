using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public struct AICardWeight
{
    public Card card;
    public int weight;
}

public enum EnemyType
{
    Normal,
    Elite,
    BossCorperateOvermind,
}

public class Enemy : Unit
{
    public EnemyType type;
    public AICardWeight[] aICardWeights;

    Dictionary<Card, int> cardCooldowns = new();

    List<Card> playedCards = new();

    Card GetWeightedRandomCardToPlay()
    {
        int cardWeightSum = 0;
        foreach (AICardWeight cardWeight in aICardWeights)
        {
            cardWeightSum += cardWeight.weight;
        }

        foreach (AICardWeight cardWeight in aICardWeights)
        {
            if (Random.Range(0, cardWeightSum) < cardWeight.weight)
            {
                return cardWeight.card;
            }
            else
            {
                cardWeightSum -= cardWeight.weight;
            }
        }

        return aICardWeights[0].card;
    }

    Card GetCardOfEnergyCost(int cost)
    {
        foreach (AICardWeight cardWeight in aICardWeights)
        {
            if (cardWeight.card.cost == cost)
            {
                return cardWeight.card;
            }
        }

        return null;
    }

    public override void StartTurn()
    {
        base.StartTurn();

        playedCards.Clear();
    }

    public Card GetRandomEligibleCardToPlay()
    {
        var eligbleActions = aICardWeights
        .Where(cardWeight => !playedCards.Contains(cardWeight.card))
        .Where(cardWeight => cardWeight.card.cost <= currentEnergy)
        .ToList();

        if (eligbleActions.Count == 0)
        {
            return null;
        }

        int totalWeight = eligbleActions.Sum(cardWeight => cardWeight.weight);
        int randomWeight = Random.Range(0, totalWeight);
        foreach (AICardWeight cardWeight in eligbleActions)
        {
            randomWeight -= cardWeight.weight;
            if (randomWeight < 0)
            {
                return cardWeight.card;
            }
        }

        return eligbleActions[Random.Range(0, eligbleActions.Count)].card;

    }

    public IEnumerator PlayTurn()
    {
        TickCooldowns();

        if (type == EnemyType.BossCorperateOvermind)
        {
            PlayCard(CorperateBossMove());
            yield return new WaitForSeconds(2f);
        }
        else
        {
            while (currentEnergy > 0)
            {
                var card = GetRandomEligibleCardToPlay();
                if (card != null)
                {
                    PlayCard(card);

                    LeanTween.moveLocal(gameObject, -transform.right * 0.5f, 0.25f).setEasePunch();

                    yield return new WaitForSeconds(2f);
                }
                else
                {
                    Debug.Log("No eligible cards to play");
                    break;
                }
            }
        }

        yield return null;
    }

    public Unit GetTarget()
    {
        // @todo: teammates, self, etc

        return Player.Instance;
    }

    public void PlayCard(Card card)
    {
        if (currentEnergy < card.cost)
        {
            Debug.Log("Not enough energy!");
            return;
        }

        playedCards.Add(card);
        currentEnergy -= card.cost;

        if (IsInCooldown(card))
        {
            Debug.LogError($"Card {card.cardName} is on cooldown!");
            return;
        }

        // Set cooldown when card is played
        if (card.cooldown > 0)
        {
            cardCooldowns[card] = card.cooldown;
        }

        Animate(card.unitAnimType);
    
        var target = GetTarget();
        ApplyCardEffect(card, this, target);
        Debug.Log($"Enemy {name} played {card.cardName} to target {target?.name}");
    }

    public override void Die()
    {
        EncounterManager.Instance.UnregisterEnemy(this);
        base.Die();
    }


    private Card CorperateBossMove()
    {
        // Priority 1: Check if player has no debuffs
        if (!StatusEffectManager.Instance.UnitHasDebuff(Player.Instance))
        {
            var stuckInMeeting = GetPlayableCardByName("Sprint Planning");
            if (stuckInMeeting != null)
            {
                Debug.Log("Player has no debuffs, playing Sprint Planning");
                return stuckInMeeting;
            }
        }

        // Priority 2: Player has 2+ block
        if (Player.Instance.block >= 2)
        {
            var overtimeDemand = GetPlayableCardByName("Overtime Demand");
            if (overtimeDemand != null)
            {
                Debug.Log("Player has 2+ block, playing Overtime Demand");
                return overtimeDemand;
            }
        }

        // Priority 3: Boss HP < 50%
        if (currentHP < maxHP / 2)
        {
            var performanceReview = GetPlayableCardByName("Performance Review");
            if (performanceReview != null)
            {
                Debug.Log("Boss HP < 50%, playing Performance Review");
                return performanceReview;
            }
        }

        // Priority 4: Player used 3+ cards
        if (Player.Instance.CardsPlayedThisTurn >= 3)
        {
            var priorityShift = GetPlayableCardByName("Priority Shift");
            if (priorityShift != null)
            {
                Debug.Log("Player used 3+ cards, playing Priority Shift");
                return priorityShift;
            }
        }

        // Priority 5: Player dealt 20+ damage
        if (Player.Instance.DamageDealtThisTurn >= 20)
        {
            var policyEnforcement = GetPlayableCardByName("Policy Enforcement");
            if (policyEnforcement != null)
            {
                Debug.Log("Player dealt 20+ damage, playing Policy Enforcement");
                return policyEnforcement;
            }
        }

        // Priority 6: Default actions
        string[] defaultMoves = { "Priority Shift", "Overtime Demand", "Performance Review" };
        var playableMoves = defaultMoves.Where(move => GetPlayableCardByName(move) != null).ToList();
        string randomMove = playableMoves[Random.Range(0, playableMoves.Count)];
        Debug.Log("Playing default move: " + randomMove);
        return GetPlayableCardByName(randomMove);
    }

    private Card GetCardByName(string cardName)
    {
        return aICardWeights
            .FirstOrDefault(weight => weight.card.cardName == cardName)
            .card;
    }

    private Card GetPlayableCardByName(string cardName)
    {
        return aICardWeights
            .FirstOrDefault(weight => weight.card.cardName == cardName && !IsInCooldown(weight.card))
            .card;
    }

    void TickCooldowns()
    {
        // Tick all cards in cooldown
        var keys = cardCooldowns.Keys.ToList();
        foreach (var card in keys)
        {
            if (cardCooldowns[card] > 0)
            {
                cardCooldowns[card]--;
                Debug.Log($"Card {card.cardName} cooldown ticked to {cardCooldowns[card]}");
            }
        }
    }

    bool IsInCooldown(Card card)
    {
        return cardCooldowns.TryGetValue(card, out int cooldown) && cooldown > 0;
    }

    public override void StartCombat()
    {
        base.StartCombat();

        // Initialize cooldowns
        cardCooldowns.Clear();
        foreach (var cardWeight in aICardWeights)
        {
            cardCooldowns[cardWeight.card] = 0;

        }
    }
}