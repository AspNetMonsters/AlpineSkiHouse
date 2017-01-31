System.register(['react', 'react-dom'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __extends = (this && this.__extends) || function (d, b) {
        for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
    var react_1, react_dom_1;
    var LockoutControl, UserRow, App;
    return {
        setters:[
            function (react_1_1) {
                react_1 = react_1_1;
            },
            function (react_dom_1_1) {
                react_dom_1 = react_dom_1_1;
            }],
        execute: function() {
            LockoutControl = (function (_super) {
                __extends(LockoutControl, _super);
                function LockoutControl() {
                    _super.apply(this, arguments);
                }
                LockoutControl.prototype.render = function () {
                    var _this = this;
                    if (this.props.lockoutEnabled === null) {
                        return react_1["default"].createElement("td", null, 
                            "Locked out ", 
                            react_1["default"].createElement("button", {onClick: function (e) { return _this.unlock(e); }}, "Unlock"));
                    }
                    else {
                        return react_1["default"].createElement("td", null, 
                            "Not locked out ", 
                            react_1["default"].createElement("button", {onClick: function (e) { return _this.lockout(e); }}, "Lockout"));
                    }
                };
                LockoutControl.prototype.lockout = function (event) {
                    fetch("/Admin/api/Users/Lock/" + this.props.user.id, { method: "PUT", credentials: "include" });
                };
                LockoutControl.prototype.unlock = function (event) {
                    fetch("/Admin/api/Users/Unlock/" + this.props.user.id, { method: "PUT", credentials: "include" });
                };
                return LockoutControl;
            }(react_1.Component));
            UserRow = (function (_super) {
                __extends(UserRow, _super);
                function UserRow() {
                    _super.apply(this, arguments);
                }
                UserRow.prototype.render = function () {
                    return react_1["default"].createElement("tr", {key: this.props.user.Id}, 
                        react_1["default"].createElement("td", null, this.props.user.id), 
                        react_1["default"].createElement("td", null, this.props.user.firstName), 
                        react_1["default"].createElement("td", null, this.props.user.lastName), 
                        react_1["default"].createElement(LockoutControl, {user: this.props.user}));
                };
                return UserRow;
            }(react_1.Component));
            App = (function (_super) {
                __extends(App, _super);
                function App() {
                    var _this = this;
                    _super.call(this);
                    this.state = { users: [] };
                    fetch("/Admin/api/Users", { credentials: "include" }).then(function (r) { return r.json(); }).then(function (users) { return _this.setState({ users: users }); });
                }
                App.prototype.render = function () {
                    return (react_1["default"].createElement("div", null, 
                        react_1["default"].createElement("h1", null, "Admin Portal"), 
                        react_1["default"].createElement("table", null, 
                            react_1["default"].createElement("thead", null, 
                                react_1["default"].createElement("tr", null, 
                                    react_1["default"].createElement("th", null, "ID"), 
                                    react_1["default"].createElement("th", null, "First Name"), 
                                    react_1["default"].createElement("th", null, "Last Name"), 
                                    react_1["default"].createElement("th", null, "Lockout Expiry"))
                            ), 
                            react_1["default"].createElement("tbody", null, this.state.users.map(function (item) {
                                return react_1["default"].createElement(UserRow, {user: item, key: item.id});
                            })))));
                };
                return App;
            }(react_1.Component));
            react_dom_1.render(react_1["default"].createElement(App, null), document.getElementById('app'));
        }
    }
});
