import Footer from "./Footer";
import Navbar from "./Navbar";
import styles from "@/styles/Home.module.css";

const Layout = ({ children } : {children: React.ReactNode}) => {
    return (
        <div className={styles.container}>
            <Navbar/>
            <main className={styles.main}>
                { children }
            </main>
            <Footer/>
        </div>
    );
}

export default Layout;