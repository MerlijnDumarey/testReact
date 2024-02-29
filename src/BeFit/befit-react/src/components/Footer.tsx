import { Typography } from "@mui/material";
import styles from "@/styles/Home.module.css";

const Footer = () => {
    return (
      <footer className={styles.footer}>
        <Typography variant="caption" display="block" gutterBottom>
          Copyright 2024 Howest
        </Typography>
      </footer>
    );
  }
   
  export default Footer;