using System.Collections;
using System.Collections.Generic;
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

    Card GetRandomCardToPlay()
    {
        return aICardWeights[Random.Range(0, aICardWeights.Length)].card;
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

        // var card = GetRandomCardToPlay();
        // if (card != null)
        // {
        //     PlayCard(card);
        // }
    }

    public IEnumerator PlayTurn()
    {
        while (currentEnergy > 0)
        {
            var card = GetRandomCardToPlay();
            if (card != null)
            {
                PlayCard(card);

                LeanTween.moveLocal(gameObject, -transform.right * 0.5f, 0.25f).setEasePunch();
            }

            yield return new WaitForSeconds(2f);
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

        currentEnergy -= card.cost;

        var target = GetTarget();
        ApplyCardEffect(card, this, target);
        Debug.Log($"Enemy {name} played {card.cardName} to target {target?.name}");
    }
}