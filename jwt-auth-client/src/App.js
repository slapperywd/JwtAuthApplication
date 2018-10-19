import React, { Component } from 'react';
import './App.css';
import NavBar from './components/NavBar';

class App extends Component {
  render() {
    return (
      <div className="App">

        <NavBar />
        <h1>App page</h1>
      </div>
    );
  }
}

export default App;
