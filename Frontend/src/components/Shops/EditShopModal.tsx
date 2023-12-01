import {
  Button,
  Modal,
  Box,
  Typography,
  Stack,
  TextField,
  Chip,
} from "@mui/material";
import { useContext, useState } from "react";
import { CreateShopParams, editShop } from "../../services/shop";
import { IShop } from "../../interfaces/Shop/IShop";
import { EditShop } from "../../interfaces/Shop/EditShop";
import { NotificationContext } from "../../utils/context/NotificationContext";

type editShopModalParams = {
  handleRefresh: () => void;
  shop: IShop;
};

const EditShopModal = ({ handleRefresh, shop }: editShopModalParams) => {
  const [open, setOpen] = useState(false);
  const { success } = useContext(NotificationContext);

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const [formData, setFormData] = useState({
    name: shop.name,
    description: shop.description,
    contactInformation: shop.contactInformation,
  });

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
  };

  const style = {
    position: "absolute" as "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    width: "50vh",
    bgcolor: "background.paper",
    p: 4,
    pb: 10,
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const newShop: EditShop = formData;
    const editedShop = await editShop({ shopId: shop.id, shop: newShop });
    if (editedShop) {
      handleRefresh();
      handleClose();
      success("Successfully edited shop!");
    }
  };

  return (
    <>
      <Button onClick={handleOpen}>Edit</Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box component="form" sx={style} onSubmit={handleSubmit}>
          <Box
            sx={{
              pb: 6,
              pt: 2,
              display: "flex",
              justifyContent: "space-between",
            }}
          >
            <Typography id="modal-modal-title" variant="h6" component="h2">
              Edit shop
            </Typography>
          </Box>
          <Box>
            <Stack spacing={2}>
              <div>
                <TextField
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  inputProps={{ maxLength: 100 }}
                  label="Name"
                  name="name"
                  value={formData.name}
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div>
                <TextField
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  inputProps={{ maxLength: 100 }}
                  label="Description"
                  name="description"
                  value={formData.description}
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div>
                <TextField
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  inputProps={{ maxLength: 100 }}
                  label="Contacts (email, discord, etc)"
                  name="contactInformation"
                  value={formData.contactInformation}
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div>
                <Box
                  sx={{
                    pt: 4,
                    display: "flex",
                    justifyContent: "space-between",
                  }}
                >
                  <Button variant="contained" type="submit">
                    Edit
                  </Button>
                </Box>
              </div>
            </Stack>
          </Box>
        </Box>
      </Modal>
    </>
  );
};
export default EditShopModal;
