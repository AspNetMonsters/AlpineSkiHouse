// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

///<reference path="../../../node_modules/@types/react/index.d.ts" />

import React, { Component } from 'react';
import { render } from 'react-dom';

class App extends Component<any,any> {
    render() {
        return (
            <h1>Admin Portal</h1>
        );
    }
}
render(
    <App />,
    document.getElementById('app')
);