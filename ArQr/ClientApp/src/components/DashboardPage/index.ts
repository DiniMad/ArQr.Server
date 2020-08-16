import {ComponentProps} from "react";

import DashboardComponent from "./Dashboard";

const Dashboard = (props: ComponentProps<any>) => DashboardComponent({isMobileSize: window.innerWidth < 760, ...props});
export default Dashboard;
