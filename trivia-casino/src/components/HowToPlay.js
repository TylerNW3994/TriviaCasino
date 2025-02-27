import { useParams } from 'react-router-dom';

function GameDetail() {
  const { gameName } = useParams();

  return (
    <div>
      <h3>How to Play {gameName}</h3>
    </div>
  );
}

export default GameDetail;