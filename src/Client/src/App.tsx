import type { Component } from 'solid-js';

import Navbar from './components/Navbar';
import { Route, Router } from '@solidjs/router';
import Home from './pages/Home';
import { AppStateProvider } from './components/AppState';
import ServerPage from './pages/ServerPage';

const App: Component = () => {
  return (
    <AppStateProvider>
      <div class='flex min-h-screen'>
        <Navbar />
        <div class='bg-gray-700 flex-auto pt-2 px-4 text-gray-300'>
          <Router>
            <Route path='/' component={Home}/>
            <Route path='/server/:id' component={ServerPage}/>
          </Router>
        </div>
      </div>
    </AppStateProvider>
  );
};

export default App;
