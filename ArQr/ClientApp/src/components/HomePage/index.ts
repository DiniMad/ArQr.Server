import HomeDesktop from './Home';
import HomeMobile from './Home.Mobile';

const Home=(window.innerWidth >= 760)?HomeDesktop:HomeMobile;
export default Home;
