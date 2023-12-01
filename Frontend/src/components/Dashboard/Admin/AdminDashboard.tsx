import { useContext } from "react";
import { AuthContext } from "../../../utils/context/AuthContext";

const AdminDashboard = () => {
  const { user } = useContext(AuthContext);

  return user && user.role && <div>Welcome, Admin!</div>;
};

export default AdminDashboard;
