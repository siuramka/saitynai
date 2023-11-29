import { useContext } from "react";
import { AuthContext } from "../../../utils/context/AuthContext";
import LayoutManager from "../../../layouts/LayoutManager";

const UserDashboard = () => {
  const { user } = useContext(AuthContext);

  return user && user.role && <div>User Dashboard</div>;
};

export default UserDashboard;
