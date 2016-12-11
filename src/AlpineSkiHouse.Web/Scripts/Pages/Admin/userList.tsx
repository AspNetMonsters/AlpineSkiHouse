// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX

///<reference path="../../../node_modules/@types/react/index.d.ts" />

import React, { Component } from 'react';
import { render } from 'react-dom';

declare var fetch: any;

class LockoutControl extends Component<any, any>{
    render() {
        if (this.props.user.lockoutEnabled) {
            return <td>
                <span className="col-md-6">
                    Locked out
                    </span>
                <span className="col-md-6">
                    <button className="btn" onClick={(e) => this.unlock(e)}>Unlock</button>
                </span>
            </td>
        }
        else {
            return <td>
                <span className="col-md-6">
                    Not locked out
                    </span>
                <span className="col-md-6">
                    <button className="btn" onClick={(e) => this.lockout(e)}>Lockout</button>
                </span>
            </td>
        }
    }
    lockout(event) {
        fetch("/Admin/api/Users/Lock/" + this.props.user.id, { method: "PUT", credentials: "include" })
            .then(() => this.props.onInvalidate());
    }
    unlock(event) {
        fetch("/Admin/api/Users/Unlock/" + this.props.user.id, { method: "PUT", credentials: "include" })
            .then(() => this.props.onInvalidate());
        
    }
}

class UserRow extends Component<any, any>{
    render() {
        return <tr key={this.props.user.Id}>
            <td>{this.props.user.id}</td>
            <td>{this.props.user.firstName}</td>
            <td>{this.props.user.lastName}</td>
            <LockoutControl user={this.props.user} onInvalidate={(e) => this.props.onInvalidate()}/>
        </tr>
    }
}

class App extends Component<any, any> {
    constructor() {
        super();
        this.state = { users: [] };
        this.getUsers();
    }
    getUsers() {
        fetch("/Admin/api/Users", { credentials: "include" }).then((r) => r.json()).then((users) => this.setState({ users: users }));
    }
    render() {
        return (
            <div>
                <h1>Admin Portal</h1>
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Is Locked</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.users.map((item) => {
                            return <UserRow user={item} key={item.id} onInvalidate={(e) => this.getUsers()} />;
                        })}
                    </tbody>
                </table>
            </div>
        );
    }
}


render(
    <App />,
    document.getElementById('app')
);

