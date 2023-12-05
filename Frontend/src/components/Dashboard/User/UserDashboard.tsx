import { useSelector } from "react-redux";
import { selectUser } from "../../../features/AuthSlice";

const UserDashboard = () => {
  const user = useSelector(selectUser);

  return user && user.role && <div>Hey user!</div>;
};

export default UserDashboard;
