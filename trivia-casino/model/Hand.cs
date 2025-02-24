using System.Collections;

namespace TriviaCasino.Model;
public class Hand : IEnumerable<Card> {
    public List<Card> cards { get; set; } = new();

    public void Add(Card card) {
        cards.Add(card);
    }

    public IEnumerator<Card> GetEnumerator() => cards.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}