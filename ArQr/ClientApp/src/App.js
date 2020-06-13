import React from 'react';
import {Route} from 'react-router';
import Home from './components/HomePage';
import Layout from './components/Layout';
import Dashboard from './components/DashboardPage';

if (window.innerWidth >= 760)
    require('./styles/style.css');
else
    require('./styles/style.mobile.css');

const App = () => {
    return (
        <>
            <Route exact path='/' component={Home}/>
            <Layout>
                {/*TODO: Change the route components below into the authorize route */}
                <Route path='/dashboard' component={Dashboard}/>
            </Layout>
        </>
    );
};

export default App;
