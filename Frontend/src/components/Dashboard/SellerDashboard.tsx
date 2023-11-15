import { useContext } from "react";
import { AuthContext } from "../../utils/context/AuthContext";
import LayoutManager from "../../layouts/LayoutManager";

const SellerDashboard = () => {
  const { user } = useContext(AuthContext);

  return (
    user &&
    user.role && (
      <LayoutManager role={user.role}>
        <div>Seller Dashboard</div>
        
      </LayoutManager>
    )
  );
};

export default SellerDashboard;
