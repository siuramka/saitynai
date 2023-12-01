import { Box, Container } from "@mui/material";
import AdminHeader from "../components/Dashboard/Admin/Header/Header";
import { LayoutProps } from "../interfaces/LayoutProps";

const AdminLayout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <>
      <AdminHeader />
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

export default AdminLayout;
