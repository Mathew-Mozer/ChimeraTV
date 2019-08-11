using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
[Serializable]
public class MmCardList
{
    public List<mmCard> cards = new List<mmCard>();
    private string[] cardList = new string[] { "AH", "AC", "AD", "AS", "2H", "2C", "2D", "2S", "3H", "3C", "3D", "3S", "4H", "4C", "4D", "4S", "5H", "5C", "5D", "5S", "6H", "6C", "6D", "6S", "7H", "7C", "7D", "7S", "8H", "8C", "8D", "8S", "9H", "9C", "9D", "9S", "JH", "JC", "JD", "JS", "QH", "QC", "QD", "QS", "KH", "KC", "KD", "KS" };
    public MmCardList()
    {

    }
    public void generateCards()
    {
        for (int i = 0; i < cardList.Length; i++)
        {
            mmCard mm = new mmCard(cardList[i]);
        }
    }
}
