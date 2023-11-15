import { LayoutProps } from "../interfaces/LayoutProps";

const AdminLayout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <div>
      Admin Layout
      {children}
    </div>
  );
};

export default AdminLayout;
