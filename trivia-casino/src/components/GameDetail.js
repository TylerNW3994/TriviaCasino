import { Link, useParams } from 'react-router-dom';

function GameDetail() {
  const { gameName } = useParams();

  return (
    <div>
      <h2>{gameName.toUpperCase()}</h2>
      <Link to={`/howtoplay/${gameName}`}>
        <button>Learn how to play {gameName}</button>
      </Link>
    </div>

    // Implement games list here
    // Create new Game button
  );
}

export default GameDetail;