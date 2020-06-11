import React from 'react';
import {Route} from 'react-router';
import Home from './components/HomePage';

if (window.innerWidth >= 760)
    require('./styles/style.css');
else
    require('./styles/style.mobile.css');

const App = () => {
    return (
        <Route exact path='/' component={Home}/>
    );
};

export default App;
