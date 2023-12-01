import { Box, Container, Paper, Typography } from "@mui/material";
import SellerHeader from "../components/Dashboard/Seller/Header/Header";

interface LayoutProps {
  children: React.ReactNode;
}

const SellerLayout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <>
      <SellerHeader />
      <Container>
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            justifyContent: "center",
            minHeight: "100vh",
          }}
        >
          {children}
        </Box>
      </Container>
    </>
  );
};

export default SellerLayout;
