import { Container } from "@mui/material";
import AdminHeader from "../components/Dashboard/Admin/Header/Header";
import { LayoutProps } from "../interfaces/LayoutProps";

const AdminLayout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <Container>
      <AdminHeader />
      {children}
    </Container>
  );
};

export default AdminLayout;
