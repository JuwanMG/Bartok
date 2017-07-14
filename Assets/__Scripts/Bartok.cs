using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bartok : MonoBehaviour {
    static public Bartok S;

    public TextAsset deckXML;
    public TextAsset layoutXML;
    public Vector3 layoutCenter = Vector3.zero;
    public bool _____________;
    public Deck deck;
    public List<CardBartok> drawPile;
    public List<CardBartok> discardPile;
    public BartokLayout layout;
    public Transform layoutAnchor;

	// Use this for initialization
	void Awake () {
        S = this;
	}
	
	// Update is called once per frame
	void Start () {
        deck = GetComponent<Deck>();
        deck.InitDeck(deckXML.text);
        Deck.Shuffle(ref deck.cards);

        layout = GetComponent<BartokLayout>();
        layout.ReadLayout(layoutXML.text);
        drawPile = UpgradeCardsList(deck.cards);

	}

    List<CardBartok> UpgradeCardsList(List<Card> lCD)
    {
        List<CardBartok> lCB = new List<CardBartok>();
        foreach(Card tCD in lCD)
        {
            lCB.Add(tCD as CardBartok);
        }
        return (lCB);
    }
}
