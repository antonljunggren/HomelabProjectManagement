import type { Component } from 'solid-js';

import logo from './logo.svg';
import styles from './App.module.css';
import Navbar from './components/Navbar';
import { Route, Router } from '@solidjs/router';
import Home from './pages/Home';

const App: Component = () => {
  return (
    <div class='flex'>
      <Navbar />
      <div class='bg-gray-700 flex-auto pt-10 text-gray-300'>
        <Router>
          <Route path='/' component={Home}/>
        </Router>
      </div>
    </div>
  );
};

export default App;
