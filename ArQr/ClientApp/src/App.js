import React from 'react';
import {Route} from 'react-router';
import {Switch} from 'react-router-dom';

import Home from './components/HomePage';
import Dashboard from './components/DashboardPage';
import Product from './components/ProductPage';
import NotFound from './components/NotFoundPage';
import withLayout from './components/higherOrderComponents/withLayout';
import AuthorizeRoute from './components/AuthorizeRoute';
import ContextProviders from './components/ContextProviders';

if (window.innerWidth >= 760)
    require('./styles/style.css');
else
    require('./styles/style.mobile.css');

const App = () => {
    return (
        <ContextProviders>
            <Switch>
                <Route exact path='/' component={Home}/>
                <AuthorizeRoute path='/dashboard' component={withLayout(Dashboard)}/>
                <AuthorizeRoute path='/product' component={withLayout(Product)}/>
                <Route path='*' component={NotFound}/>
            </Switch>
        </ContextProviders>
    );
};

export default App;
