import { Link } from 'react-router-dom';

function Games() {
  const gamesList = [
    { id: 'blackjack', name: 'Blackjack' },
    { id: 'poker', name: 'Poker' }
  ];

  return (
    <div>
      <h1>Games Lobby</h1>
      <ul>
        {gamesList.map((game) => (
          <li key={game.id}>
            <Link to={`/games/${game.id}`}>{game.name}</Link>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default Games;