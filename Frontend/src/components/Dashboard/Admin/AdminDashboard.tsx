import { useSelector } from "react-redux";
import { selectUser } from "../../../features/AuthSlice";

const AdminDashboard = () => {
  const user = useSelector(selectUser);

  return user && user.role && <div>Hey admin!</div>;
};

export default AdminDashboard;
