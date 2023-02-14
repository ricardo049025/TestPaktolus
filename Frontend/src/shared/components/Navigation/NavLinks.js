import React from 'react';
import { NavLink } from 'react-router-dom';
import './NavLinks.css';

const NavLinks = props => {


  return (
    <ul className="nav-links">
      <li>
        <NavLink to="/" exact>
          STUDENTS
        </NavLink>
      </li>
      <li>
        <NavLink to="/hobby" exact>
          ADD HOBBY
        </NavLink>
      </li>
    </ul>
  );
};

export default NavLinks;
