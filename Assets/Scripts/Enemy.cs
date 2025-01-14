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


public class Enemy : Unit
{
    public AICardWeight[] aICardWeights;

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

        var target = GetTarget();
        ApplyCardEffect(card, this, target);
        Debug.Log($"Enemy {name} played {card.cardName} to target {target?.name}");
    }

    public override void Die()
    {
        EncounterManager.Instance.UnregisterEnemy(this);
        base.Die();
    }
}