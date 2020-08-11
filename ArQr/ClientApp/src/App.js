import React from 'react';
import {Route} from 'react-router';
import {Switch} from 'react-router-dom';

import Home from './components/HomePage';
import Dashboard from './components/DashboardPage';
import Product from './components/ProductPage';
import NotFound from './components/NotFoundPage';
import withLayout from './components/higherOrderComponents/withLayout';
import AccessTokenProvider from './components/contexts/AccessToken';

if (window.innerWidth >= 760)
    require('./styles/style.css');
else
    require('./styles/style.mobile.css');

const App = () => {
    return (
        <Switch>
            <AccessTokenProvider>
                <Route exact path='/' component={Home}/>
                {/*TODO: Change the route components below into the authorize route */}
                <Route path='/dashboard' component={withLayout(Dashboard)}/>
                <Route path='/product' component={withLayout(Product)}/>
                <Route path='*' component={NotFound}/>
            </AccessTokenProvider>
        </Switch>
    );
};

export default App;
