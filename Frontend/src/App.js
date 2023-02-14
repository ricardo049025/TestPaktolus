import './App.css';
import {Route,BrowserRouter as Router,Switch,Redirect} from "react-router-dom";
import MainNavigation from './shared/components/Navigation/MainNavigation';
import NewStudent from './student/pages/NewStudent';
import UpdateStudent from './student/pages/UpdateStudent';
import NewHobby from './hobbies/pages/NewHobby';

const App = () => {
  return(
    <Router>  
      <MainNavigation />
      <main>
        <Switch>
          <Route path="/" exact><NewStudent /></Route>
          <Route path="/student/:studentId" exact><UpdateStudent /></Route>
          <Route path="/hobby" exact><NewHobby /></Route>
          <Redirect to="/" />
        </Switch>
      </main>
    </Router>
  )

}

export default App;
