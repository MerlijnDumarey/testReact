import * as React from 'react';
import { Typography } from '@mui/material';
import { Test, Category } from '@/models/models';
import { Mocking } from '@/services/mockDataService';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemText from '@mui/material/ListItemText';
import Checkbox from '@mui/material/Checkbox';

interface TestsListProps {
    selectedTests: Test[];
    filteredTests: Test[];
    onSelectedTestsChange: (selectedTests: Test[]) => void;
}

const TestsList: React.FC<TestsListProps> = ({selectedTests, filteredTests, onSelectedTestsChange}) => {


    const handleCheckboxChange = (test: Test) => {
        const isSelected = selectedTests.some((st) => st.id === test.id);

        if (isSelected) {
            selectedTests = selectedTests.filter((st) => st.id !== test.id);
        }
        else {
            selectedTests = [...selectedTests, test];
        }
        onSelectedTestsChange(selectedTests);
    }

    return(
        <div>
                <Typography align='center'>
                    {selectedTests.length} geselecteerd
                </Typography>
            <List>
            {
                filteredTests.map((test) => (
                <ListItem key={test.id}>
                    <ListItemButton>
                        <ListItemIcon>
                            <Checkbox
                            edge="start"
                            tabIndex={-1}
                            disableRipple
                            checked={selectedTests.some((selectedTest) => selectedTest.id === test.id)}
                            onChange={() => handleCheckboxChange(test)}
                            />
                        </ListItemIcon>
                        <ListItemText primary={test.name} secondary={test.categoryId}/>
                    </ListItemButton>
                </ListItem>))
            }
            </List>
        </div>
    );
}

export default TestsList;