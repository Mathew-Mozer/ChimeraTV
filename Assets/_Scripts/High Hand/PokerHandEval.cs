using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PokerHandEval : MonoBehaviour {
  public static List<string> PGhands = new List<string> { "22233", "22244", "22255", "22266", "22277", "22288", "22299", "222TT", "222JJ", "222QQ", "222KK", "222AA", "33322", "33344", "33355", "33366", "33377", "33388", "33399", "333TT", "333JJ", "333QQ", "333KK", "333AA", "44422", "44433", "44455", "44466", "44477", "44488", "44499", "444TT", "444JJ", "444QQ", "444KK", "444AA", "55522", "55533", "55544", "55566", "55577", "55588", "55599", "555TT", "555JJ", "555QQ", "555KK", "555AA", "66622", "66633", "66644", "66655", "66677", "66688", "66699", "666TT", "666JJ", "666QQ", "666KK", "666AA", "77722", "77733", "77744", "77755", "77766", "77788", "77799", "777TT", "777JJ", "777QQ", "777KK", "777AA", "88822", "88833", "88844", "88855", "88866", "88877", "88899", "888TT", "888JJ", "888QQ", "888KK", "888AA", "99922", "99933", "99944", "99955", "99966", "99977", "99988", "999TT", "999JJ", "999QQ", "999KK", "999AA", "TTT22", "TTT33", "TTT44", "TTT55", "TTT66", "TTT77", "TTT88", "TTT99", "TTTJJ", "TTTQQ", "TTTKK", "TTTAA", "JJJ22", "JJJ33", "JJJ44", "JJJ55", "JJJ66", "JJJ77", "JJJ88", "JJJ99", "JJJTT", "JJJQQ", "JJJKK", "JJJAA", "QQQ22", "QQQ33", "QQQ44", "QQQ55", "QQQ66", "QQQ77", "QQQ88", "QQQ99", "QQQTT", "QQQJJ", "QQQKK", "QQQAA", "KKK22", "KKK33", "KKK44", "KKK55", "KKK66", "KKK77", "KKK88", "KKK99", "KKKTT", "KKKJJ", "KKKQQ", "KKKAA", "AAA22", "AAA33", "AAA44", "AAA55", "AAA66", "AAA77", "AAA88", "AAA99", "AAATT", "AAAJJ", "AAAQQ", "AAAKK", "22223", "22223", "22224", "22225", "22226", "22227", "22228", "22229", "2222T", "2222J", "2222Q", "2222K", "2222A", "33332", "33334", "33335", "33336", "33337", "33338", "33339", "3333T", "3333J", "3333Q", "3333K", "3333A", "44442", "44443", "44445", "44446", "44447", "44448", "44449", "4444T", "4444J", "4444Q", "4444K", "4444A", "55552", "55553", "55554", "55556", "55557", "55558", "55559", "5555T", "5555J", "5555Q", "5555K", "5555A", "66662", "66663", "66664", "66665", "66667", "66668", "66669", "6666T", "6666J", "6666Q", "6666K", "6666A", "77772", "77773", "77774", "77775", "77776", "77778", "77779", "7777T", "7777J", "7777Q", "7777K", "7777A", "88882", "88883", "88884", "88885", "88886", "88887", "88889", "8888T", "8888J", "8888Q", "8888K", "8888A", "99992", "99993", "99994", "99995", "99996", "99997", "99998", "9999T", "9999J", "9999Q", "9999K", "9999A", "TTTT2", "TTTT3", "TTTT4", "TTTT5", "TTTT6", "TTTT7", "TTTT8", "TTTT9", "TTTTJ", "TTTTQ", "TTTTK", "TTTTA", "JJJJ2", "JJJJ3", "JJJJ4", "JJJJ5", "JJJJ6", "JJJJ7", "JJJJ8", "JJJJ9", "JJJJT", "JJJJQ", "JJJJK", "JJJJA", "QQQQ2", "QQQQ3", "QQQQ4", "QQQQ5", "QQQQ6", "QQQQ7", "QQQQ8", "QQQQ9", "QQQQT", "QQQQJ", "QQQQK", "QQQQA", "KKKK2", "KKKK3", "KKKK4", "KKKK5", "KKKK6", "KKKK7", "KKKK8", "KKKK9", "KKKKT", "KKKKJ", "KKKKQ", "KKKKA", "AAAA2", "AAAA3", "AAAA4", "AAAA5", "AAAA6", "AAAA7", "AAAA8", "AAAA9", "AAAAT", "AAAAJ", "AAAAQ", "AAAAK", "23456", "34567", "45678", "56789", "6789T", "789TJ", "89TJQ", "9TJQK", "A2345", "TJQKA", "AAAAA" };
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static int PokerHandRank(string hand)
    {
        return PGhands.FindIndex(x => x.Equals(hand));
    }
}
