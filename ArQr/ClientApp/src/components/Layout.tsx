import React, {ReactChild} from "react";

import Navbar from "./NavBar";

type props = {
    children: ReactChild;
}
const Layout = ({children}: props) => {
    return (
        <main>
            <div id="content">
                {children}
            </div>
            <Navbar/>
        </main>
    );
};

export default Layout;