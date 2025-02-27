import { Link } from 'react-router-dom';

function Games() {
  const gamesList = [
    { name: 'blackjack', label: 'Blackjack' },
    { name: 'poker', label: 'Poker' }
  ];

  return (
    <div>
      <h1>Games Lobby</h1>
      <ul>
        {gamesList.map((game) => (
          <li key={game.id}>
            <Link to={`/games/${game.name }`}>{game.label}</Link>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default Games;