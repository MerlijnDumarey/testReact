import * as React from 'react';
import { Test } from '@/models/models';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';
import { Mocking } from '@/services/mockDataService';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemText from '@mui/material/ListItemText';

interface SelectedTestsListProps {
    selectedTests: Test[];
    onSelectedTestsChange: (selectedTests: Test[]) => void;
}

const SelectedTestsList: React.FC<SelectedTestsListProps> = ({selectedTests, onSelectedTestsChange}) => {
    const unselectTest = (test: Test) => {
        selectedTests = selectedTests.filter(t => t.id !== test.id);
        onSelectedTestsChange(selectedTests);
    };
    
    return (
        <div>
            Geselecteerde testen
            <List>
                {
                    selectedTests.map((test) =>
                    <ListItem
                    key={test.id}
                    secondaryAction={
                        <IconButton edge="end" aria-label="delete" onClick={() => unselectTest(test)}>
                        <DeleteIcon />
                        </IconButton>
                    }
                    >
                    <ListItemText
                        primary={test.name}
                    />
                    </ListItem>
                    )
                }
            </List>
        </div>
    );
}

export default SelectedTestsList;