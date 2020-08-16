import React, {ReactChild} from "react";

import Navbar from "./NavBar";

type Props = {
    children: ReactChild;
}
const Layout = ({children}: Props) => {
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