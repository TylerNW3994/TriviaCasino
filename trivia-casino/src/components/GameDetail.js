import { Link, useParams } from 'react-router-dom';

function GameDetail() {
  const { gameName } = useParams();

  return (
    <div>
      <h2>{gameName.toUpperCase()}</h2>
      <Link to={`/howtoplay/${gameName}`}>
        <button>Learn how to play {gameName}</button>
      </Link>
      <Link to={`/play/${gameName}`}>
        <button>Play new game</button>
      </Link>
    </div>

    // Implement games list here
  );
}

export default GameDetail;
