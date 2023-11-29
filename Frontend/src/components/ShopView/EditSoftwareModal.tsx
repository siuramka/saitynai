import {
  Button,
  Modal,
  Box,
  Typography,
  Stack,
  TextField,
} from "@mui/material";
import { useState } from "react";
import { ISoftware } from "../../interfaces/Software/ISoftware";
import { editSoftware } from "../../services/software";
import { EditSoftware } from "../../interfaces/Software/EditSoftware";

type EditSoftwareModalParams = {
  handleRefresh: () => void;
  software: ISoftware;
  shopId: number;
};

const EditSoftwareModal = ({
  handleRefresh,
  software,
  shopId,
}: EditSoftwareModalParams) => {
  const [open, setOpen] = useState(false);

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const [formData, setFormData] = useState({
    Name: software.name,
    Description: software.description,
    PriceMonthly: software.priceMonthly || 0,
    Website: software.website,
    Instructions: software.instructions,
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
    const newSoftware: EditSoftware = formData;
    const editedSoftware = await editSoftware({
      shopId: shopId,
      softwareId: software.id,
      software: newSoftware,
    });
    if (editedSoftware) {
      handleRefresh();
      handleClose();
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
              Edit software
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
                  name="Name"
                  value={formData.Name}
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
                  name="Description"
                  value={formData.Description}
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
                  label="Price monthly"
                  value={formData.PriceMonthly}
                  name="PriceMonthly"
                  type="number"
                  onChange={handleInputChange}
                />
              </div>
              <div>
                <TextField
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  inputProps={{ maxLength: 100 }}
                  label="Website"
                  name="Website"
                  value={formData.Website}
                  onChange={handleInputChange}
                  required
                />
              </div>
              <div>
                <TextField
                  multiline
                  sx={{ width: "100%" }}
                  fullWidth
                  id="outlined-size-normal"
                  inputProps={{ maxLength: 256 }}
                  label="Installation instructions"
                  name="Instructions"
                  value={formData.Instructions}
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
export default EditSoftwareModal;
