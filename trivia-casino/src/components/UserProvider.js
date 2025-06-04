import React, { createContext, useContext, useState, useEffect } from 'react';
// import axios from 'axios';

const UserContext = createContext();

export const UserProvider = ({ children }) => {
  const [user, setUser] = useState(() => {
    const storedUser = localStorage.getItem('user');
    return storedUser ? JSON.parse(storedUser) : null;
  });
  const [token, setToken] = useState(() => localStorage.getItem('token'));

  useEffect(() => {
    if (token) {
      // axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    } else {
      // delete axios.defaults.headers.common['Authorization'];
    }
  }, [token]);

  const login = (userData) => {
    setToken(userData.token);
    setUser(userData.currentPlayer);
    localStorage.setItem('token', userData.token);
    localStorage.setItem('user', JSON.stringify(userData.currentPlayer));
  };

  const logout = () => {
    setToken(null);
    setUser(null);
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  };

  return (
    <UserContext.Provider value={{ user, token, login, logout }}>
      {children}
    </UserContext.Provider>
  );
};

export const useUser = () => useContext(UserContext);
