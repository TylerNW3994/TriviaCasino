import React, { createContext, useContext, useState } from 'react';

const GameSessionContext = createContext();

export function GameSessionProvider({ children }) {
  const [sessionData, setSessionData] = useState(null);

  return (
    <GameSessionContext.Provider value={{ sessionData, setSessionData }}>
      {children}
    </GameSessionContext.Provider>
  );
}

export function useGameSession() {
  return useContext(GameSessionContext);
}