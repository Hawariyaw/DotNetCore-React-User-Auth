import "./App.css"
import { Route, BrowserRouter as Router, Switch } from "react-router-dom"
import WebLayout from "./components/web-layout"
import Login from "./components/login"
import Register from "./components/register"
import Dashboard from "./components/dashboard"
const GuestRoute = ({ component, ...rest }) => {
  return (
    <Route {...rest} render={() => <WebLayout>{component}</WebLayout>}></Route>
  )
}

function App() {
  return (
    <Router>
      <Switch>
        <GuestRoute exact path="/" component={<Register />} />
        <GuestRoute exact path="/login" component={<Login />} />
        <GuestRoute exact path="/dashboard" component={<Dashboard />} />
        <GuestRoute exact path="/register" component={<Register />} />
      </Switch>
    </Router>
  )
}

export default App
