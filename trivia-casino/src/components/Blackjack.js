import { useParams } from 'react-router-dom';

function Blackjack () {
  const { gameId } = useParams();

  return (
    <div>
      <h2>Blackjack</h2>
    </div>
  );
}

export default Blackjack;