import React from 'react';

import {Link} from 'react-router-dom';

//stateless component which has its own state is its not needed
function NavBar() {
  return (
    <nav className="navbar navbar-dark bg-primary fixed-top">
      <Link className="navbar-brand" to="/">
        Q&App
      </Link>
    </nav>
  );
}

export default NavBar;