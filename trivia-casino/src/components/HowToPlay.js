import { useParams } from 'react-router-dom';

const blackjackRules = <section>
<p>The goal is to have a hand value closer to 21 than the dealer, without exceeding 21.</p>
<ul>
  <li>Each player is dealt two cards, face up.</li>
  <li>The dealer also gets two cards: one face up and one face down.</li>
  <li>Cards 2 through 10 are worth their face value. Jacks, Queens, and Kings are worth 10 points, and Aces can be worth either 1 or 11.</li>
  <li>Players can choose to "Hit" to receive another card or "Stand" to keep their current hand.</li>
  <li>If your hand value exceeds 21, you bust and lose the round.</li>
  <li>After players stand, the dealer reveals the hidden card and must hit until the hand totals at least 17.</li>
  <li>The winner is determined by whose hand is closest to 21 without going over.</li>
</ul>
</section>

function HowToPlay() {
  const { gameName } = useParams();

  const gameRules = rules[gameName.toLowerCase()] || <p>No rules found for this game.</p>

  return (
    <div>
      <h3>How to Play {gameName}</h3>
      {gameRules}
    </div>
  );
}

const rules = {
  blackjack: blackjackRules
}

export default HowToPlay;