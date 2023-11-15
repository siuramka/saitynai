import { useContext } from "react";
import { AuthContext } from "../../utils/context/AuthContext";
import LayoutManager from "../../layouts/LayoutManager";

const AdminDashboard = () => {
  const { user } = useContext(AuthContext);

  return (
    user &&
    user.role && (
      <LayoutManager role={user.role}>
        <div>Admin Dashboard</div>
      </LayoutManager>
    )
  );
};

export default AdminDashboard;
