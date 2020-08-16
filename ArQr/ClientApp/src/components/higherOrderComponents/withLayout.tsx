import React, {ComponentProps, ComponentType} from "react";

import Layout from "../Layout";

const withLayout = (Component: ComponentType) => (props: ComponentProps<any>) => (
    <Layout>
        <Component {...props} />
    </Layout>
);

export default withLayout;