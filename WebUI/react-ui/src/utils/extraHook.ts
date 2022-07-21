import { useOutletContext } from 'react-router-dom';

export function usePageName(name: string) {
    const set = useOutletContext<React.Dispatch<React.SetStateAction<string>>>();
    set(name);
    return set;
}