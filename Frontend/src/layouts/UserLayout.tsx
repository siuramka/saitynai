import { Box, Container } from "@mui/material";
import UserHeader from "../components/Dashboard/User/Header/Header";

interface LayoutProps {
  children: React.ReactNode;
}

const UserLayout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <>
      <UserHeader />
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

export default UserLayout;
