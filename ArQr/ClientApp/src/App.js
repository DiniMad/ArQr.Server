import React from 'react';
import {Route} from 'react-router';
import {Switch} from 'react-router-dom';

import Home from './components/HomePage';
import Layout from './components/Layout';
import Dashboard from './components/DashboardPage';
import Product from './components/ProductPage';

if (window.innerWidth >= 760)
    require('./styles/style.css');
else
    require('./styles/style.mobile.css');

const App = () => {
    return (
        <Switch>
            <Route exact path='/' component={Home}/>
            <Layout>
                {/*TODO: Change the route components below into the authorize route */}
                <Route path='/dashboard' component={Dashboard}/>
                <Route path='/product' component={Product}/>
            </Layout>
        </Switch>
    );
};

export default App;
