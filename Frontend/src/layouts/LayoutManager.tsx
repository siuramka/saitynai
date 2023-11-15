// LayoutChooser.jsx

import AdminLayout from "./AdminLayout";
import SellerLayout from "./SellerLayout";
import UserLayout from "./UserLayout";

interface LayoutChooserProps {
  role: string;
  children: React.ReactNode;
}

const LayoutManager: React.FC<LayoutChooserProps> = ({ role, children }) => {
  switch (role) {
    case "Admin":
      return <AdminLayout>{children}</AdminLayout>;
    case "ShopUser":
      return <UserLayout>{children}</UserLayout>;
    case "ShopSeller":
      return <SellerLayout>{children}</SellerLayout>;
  }
};

export default LayoutManager;
