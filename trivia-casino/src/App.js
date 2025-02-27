import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import './App.css';
import Home from './components/Home';
import Games from './components/Games';
import GameDetail from './components/GameDetail';

function App() {
  return (
    <Router>
    <div className="App">
      <nav className="App-nav">
        <Link to="/">Home</Link>
        <Link to="/games">Games</Link>
      </nav>

      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/games" element={<Games />} />
        <Route path="/games/:gameName" element={<GameDetail />} />
      </Routes>
    </div>
  </Router>
  );
}

export default App;
