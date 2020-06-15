import DashboardComponent from './Dashboard';

const Dashboard = (props) => DashboardComponent({handleButton: window.innerWidth < 760, ...props});
export default Dashboard;
