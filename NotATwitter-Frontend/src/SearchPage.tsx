import SearchForm from "./SearchForm";
import Message from "./Message";
import * as React from "react";


const SearchPage = () => {
    return (
        <>
            <SearchForm className={"mt-2 mb-5"}/>

            {Array.from({ length: 100 }).map(() => {
                return <Message className="my-3"/>;
            })}
        </>
    );
}

export default SearchPage;