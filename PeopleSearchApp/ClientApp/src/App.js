import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { PeopleList } from './components/PeopleList';
import { Person } from './components/Person';

export default class App extends Component {
    displayName = App.name

    constructor(props) {
        super(props);
        this.state = { loading: true };
    }

    render() {
        return (
            <Layout>
                <Route exact path="/" component={PeopleList} />
                <Route exact path="/person" component={Person} />
                <Route exact path="/person/:id" component={Person} />
            </Layout>
        );
    }
}
